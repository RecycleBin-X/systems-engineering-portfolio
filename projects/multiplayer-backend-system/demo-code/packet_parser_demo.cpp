BOOL CPacket::CopyFromUserDataArea(void *pData, WORD wSize)
{
	WORD wTempSize = 0;
	BOOL bRet = FALSE;

	assert(pData);
	assert(wSize <= (LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE));

	wTempSize = *(WORD*)(mIOBuffer.buffer + PROTOCOL_DATALEN_FIELD_INDEX);
	if (wSize == wTempSize)
	{
		if (pData && wSize <= (LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE) && 
			wSize <= mIOBuffer.len - PROTOCOL_NON_USER_AREA_SIZE)
		{
			memcpy(pData, mIOBuffer.buffer + PROTOCOL_USER_AREA_INDEX, wSize);
			bRet = TRUE;
		}
	}
	else
	{
		bRet = FALSE;
	}

	return bRet;
}