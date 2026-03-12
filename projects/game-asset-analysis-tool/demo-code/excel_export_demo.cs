if (Program.sm_bExtractWithMacro)
{
	excelObj.Workbook.CreateVBAProject();
}
var defineWorksheet = excelObj.Workbook.Worksheets.Add("define");
var dataWorksheet = excelObj.Workbook.Worksheets.Add("DATA");
dataWorksheet.Select();

dataWorksheet.Cells.Style.Font.Size = Program.sm_fDefaultFontSize; // Default font size for whole sheet
dataWorksheet.Cells.Style.Font.Name = Program.sm_sDefaultFontName; // Default Font name for whole sheet -> Calibri causing lag for macOS

defineWorksheet.Cells.Style.Font.Size = Program.sm_fDefaultFontSize; // Default font size for whole sheet
defineWorksheet.Cells.Style.Font.Name = Program.sm_sDefaultFontName; // Default Font name for whole sheet -> Calibri causing lag for macOS

if (Program.sm_bUseDarkMode)
{
	Program.Println();
	Program.Println("Setting dark mode visual effect...");

	dataWorksheet.Cells.Style.Font.Color.SetColor(255, 229, 229, 229);
	dataWorksheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
	dataWorksheet.Cells.Style.Fill.BackgroundColor.SetColor(255, 38, 38, 38);

	defineWorksheet.Cells.Style.Font.Color.SetColor(255, 229, 229, 229);
	defineWorksheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
	defineWorksheet.Cells.Style.Fill.BackgroundColor.SetColor(255, 38, 38, 38);
}

{
    defineWorksheet.Column(DEFINE_HEADER_INFO_START_COLUMN + 1).AutoFit();
    defineWorksheet.Column(DEFINE_HEADER_INFO_START_COLUMN + 2).AutoFit();
    defineWorksheet.Column(DEFINE_HEADER_INFO_START_COLUMN + 3).AutoFit();

    defineWorksheet.Column(DEFINE_TYPE_INFO_START_COLUMN + 1).AutoFit();
    defineWorksheet.Column(DEFINE_TYPE_INFO_START_COLUMN + 2).AutoFit();
    defineWorksheet.Column(DEFINE_TYPE_INFO_START_COLUMN + 3).AutoFit();
}

//create a range for the table
ExcelRange dataRange = dataWorksheet.Cells[1, 1, tHeader.nData + 1, tHeader.nFields + 1];
ExcelRange defineInfoTableRange = defineWorksheet.Cells[DEFINE_INFO_START_ROW, DEFINE_INFO_START_COLUMN, DEFINE_INFO_START_ROW + 1, DEFINE_INFO_START_COLUMN + 1];
ExcelRange defineHeaderInfoRange = defineWorksheet.Cells[DEFINE_HEADER_INFO_START_ROW, DEFINE_HEADER_INFO_START_COLUMN, DEFINE_HEADER_INFO_START_ROW + MAX_TABLE_HEADER_COUNT, DEFINE_HEADER_INFO_START_COLUMN + 3];
ExcelRange dataTypeInfoRange = defineWorksheet.Cells[DEFINE_TYPE_INFO_START_ROW, DEFINE_TYPE_INFO_START_COLUMN, DEFINE_TYPE_INFO_START_ROW + MAX_TABLE_DATA_TYPE_COUNT, DEFINE_TYPE_INFO_START_COLUMN + 2];

//add a table to the range
ExcelTable dataTable = dataWorksheet.Tables.Add(dataRange, "DataTable");
ExcelTable defineInfoTable = defineWorksheet.Tables.Add(defineInfoTableRange, "DefineInfoTable");
ExcelTable defineHeaderInfoTable = defineWorksheet.Tables.Add(defineHeaderInfoRange, "DefineHeaderInfoTable");
ExcelTable dataTypeInfoTable = defineWorksheet.Tables.Add(dataTypeInfoRange, "DataTypeInfoTable");

//format the table
if (Program.sm_bUseDarkMode)
{
    dataTable.TableStyle = TableStyles.Medium15;
    defineInfoTable.TableStyle = TableStyles.Medium15;
    defineHeaderInfoTable.TableStyle = TableStyles.Medium15;
    dataTypeInfoTable.TableStyle = TableStyles.Medium15;
}
else
{
    dataTable.TableStyle = TableStyles.Medium16;
    defineInfoTable.TableStyle = TableStyles.Medium2;
    defineHeaderInfoTable.TableStyle = TableStyles.Medium2;
    dataTypeInfoTable.TableStyle = TableStyles.Medium2;
}

{
    defineInfoTable.ShowFilter = false;
    defineHeaderInfoTable.ShowFilter = false;
    dataTypeInfoTable.ShowFilter = false;
}

if (nMatchTablePattern != -1 && nFreezeColumnIndex != -10000)
{
    dataWorksheet.View.FreezePanes(2, nFreezeColumnIndex + 3);
}
else
{
    dataWorksheet.View.FreezePanes(2, 1);
}