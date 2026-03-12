GUL::SessionEvent* const SessionManager::OnCreateSession() 
{
	GUL_Scope_Lock(session_section_);
	
	ServerSession* const server_session = session_pool_.construct();
	session_map_.SetAt(server_session, server_session);	

	return dynamic_cast<GUL::SessionEvent*>(server_session);
}

void SessionManager::DeleteSession(ServerSession* const session)
{
	GUL_Scope_Lock(session_section_);

	SESSION_MAP::CPair* const pair = session_map_.Lookup(session);	
	
	GUL_ASSERT(pair != NULL);
	if (pair != NULL)
	{		
		session_pool_.destroy(session);
		session_map_.RemoveKey(session);
	}	
}

bool SessionManager::IsSession(ServerSession* const session)
{
	GUL_Scope_Lock(session_section_);

	SESSION_MAP::CPair* const pair = session_map_.Lookup(session);

	return pair != NULL;
}

bool SessionManager::ValidSession(ServerSession* session)
{
	GUL_Scope_Lock(session_section_);

	SESSION_MAP::CPair* const pair = session_map_.Lookup(session);

	return pair != NULL;
}

void SessionManager::OnMessage(const GUL_CHAR* const message, const DWORD last_error)
{
	if (last_error == ERROR_SUCCESS) {
		GUL_LOG_INFO(GUL_LOG_TYPE_INFO, message);
	} else {
		GUL_LOG_ERROR(GUL_LOG_TYPE_ERROR, GUL_TEXT("%s(%u)"), message, last_error);
	}
}