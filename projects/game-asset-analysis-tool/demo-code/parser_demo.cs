Program.Verbose("Info: Reading Field Names...");
for (var i = 0; i < MAX_TABLE_HEADER_COUNT; i++)
{
    tHeader.FieldNAM[i] = Program.BArrToStr(reader.ReadBytes(64));
    if (string.IsNullOrEmpty(tHeader.FieldNAM[i]))
        tHeader.FieldNAM_Excel[i] = "noname";
    else if (tHeader.FieldNAM[i].StartsWith("_"))
        tHeader.FieldNAM_Excel[i] = tHeader.FieldNAM[i].Substring(1);
    else
        tHeader.FieldNAM_Excel[i] = tHeader.FieldNAM[i];

    // Check duplicated field name
    if (tHeader.dicFieldInfo.TryGetValue(tHeader.FieldNAM_Excel[i], out var field_info))
    {
        field_info.nCount++;
        tHeader.FieldNAM_Excel[i] += "_" + field_info.nCount.ToString();
    }

    var new_field_info = new Asset_FieldInfo();
    new_field_info.nIndex = i;
    new_field_info.nCount = 1;

    tHeader.dicFieldInfo.Add(tHeader.FieldNAM_Excel[i], new_field_info);

    for (var nPatternIdx = 0; nPatternIdx < Program.sm_lstFilePatterns.Count; nPatternIdx++)
    {
        if (tHeader.nFields < Program.sm_lstFilePatterns[nPatternIdx].nMinNumFields)
            continue;

        var szUpper = tHeader.FieldNAM[i].ToUpper();
        if (szUpper.Equals(Program.sm_lstFilePatterns[nPatternIdx].strColumn))
        {
            nMatchTablePattern = nPatternIdx;
            if (Program.sm_lstFilePatterns[nPatternIdx].nFreezeColumnIdx != -10000)
            {
                nFreezeColumnIndex = i + Program.sm_lstFilePatterns[nPatternIdx].nFreezeColumnIdx;
            }
            bAddSQL = Program.sm_lstFilePatterns[nPatternIdx].bAddSQL;
            break;
        }
    }
}