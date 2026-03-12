void CClientLib::OnSocketEvent(WPARAM wParam, LPARAM lParam)
{
	assert(m_pHandler);

	DWORD iRecvSize = 0;
	static char buffer[LENGTH_SOCKET_BUFFER * 2];

	EnterCriticalSection(&m_cs);

	if(INVALID_SOCKET != m_sockClient)
	{
		switch( WSAGETSELECTEVENT(lParam) )
		{
			case FD_CONNECT:
			{
				m_bConnected = TRUE;
				m_bTryConnect = FALSE;

				if(m_pHandler)
				{
					m_pHandler->OnConnect();
				}			
			}
			break;

			case FD_READ:
			{
				memset(buffer, 0x00, sizeof(buffer));

				if(SOCKET_ERROR == (iRecvSize = recv(m_sockClient, buffer, LENGTH_SOCKET_BUFFER, 0)))
				{
					int iError = WSAGetLastError();
				}
				else
				{
					assert((m_iRecvBufferLen + iRecvSize) < sizeof(m_szRecvBuffer));

					if((m_iRecvBufferLen + iRecvSize) < sizeof(m_szRecvBuffer)) // Should not exceed the size of array
					{
						// Concatenate newly arrived msg at the end of the recv buffer
						CopyMemory(m_szRecvBuffer + m_iRecvBufferLen, buffer, iRecvSize);
						m_iRecvBufferLen += iRecvSize;
					}
					else
					{
						LOGV_ERROR("(m_iRecvBufferLen + iRecvSize) >= sizeof(m_szRecvBuffer)");
					}

					if(m_pHandler)
					{
						int recvedSize;

						while(m_pHandler->RecvBufferHasCompletePacket(m_szRecvBuffer, m_iRecvBufferLen))
						{
							recvedSize = m_pHandler->OnReceive(m_szRecvBuffer);
							
							if( recvedSize == -1 )
							{
								LOGV_ERROR("ClientLib::OnSocketEvent() OnReceive recvedSize == -1");
								Disconnect();

								LeaveCriticalSection(&m_cs);
								return;
							}

							else
							{
								// Move the rest of the memory to the very front of the buffer
								MoveMemory(m_szRecvBuffer, m_szRecvBuffer + recvedSize, m_iRecvBufferLen - recvedSize);
								m_iRecvBufferLen -= recvedSize;
							}
						}
					}
				}
			}
			break;

			case FD_CLOSE:
			{
				LOGV_ERROR("ClientLib::OnSocketEvent() FD_CLOSE");
				Disconnect();
			}
			break;
		}
	}

	LeaveCriticalSection(&m_cs);
}