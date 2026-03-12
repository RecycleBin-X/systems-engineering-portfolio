internal class ChromiumProxy
{
    private Form m_Form;

    public ChromiumProxy(Form form)
    {
        m_Form = form;
    }
	
	public List<tagArea> GetAreaList()
    {
        if (m_Form is not BrowserSubForm)
            return null;

        var subForm = (BrowserSubForm)m_Form;
        if (subForm.SubFormType != SubFormType.ChooseRegion)
            return null;

        var userData = (ChooseRegionUserData)subForm.UserData;
        if (userData == null)
            return null;

        return AreaDataManager.GetInstance().GetAreaList(userData.appId);
    }

    public List<tagArea> GetRecentAreaList()
    {
        if (m_Form is not BrowserSubForm)
            return null;

        var subForm = (BrowserSubForm)m_Form;
        if (subForm.SubFormType != SubFormType.ChooseRegion)
            return null;

        var userData = (ChooseRegionUserData)subForm.UserData;
        if (userData == null)
            return null;

        return AreaDataManager.GetInstance().GetRecentAreaList(userData.appId);
    }
}