namespace sqlsrv
{
  /// <summary>
  /// stg_bibliographyより生成されたクラス
  /// </summary>
  internal class stg_bibliography : TableRow
  {
    public required int bib_id { get; set; }
    public required string bib_code { get; set; }
    public int? bib_interlibrary_import_library_id { get; set; }
    public int? bib_interlibrary_import_bib_id { get; set; }
    public int? bib_material_id { get; set; }
    public int? bib_language_id { get; set; }
    public required string bib_alternative_symbol { get; set; }
    public int? bib_class_symbol_id { get; set; }
    public int? bib_class_symbol_secondary_id { get; set; }
    public required string bib_book_symbol { get; set; }
    public required string bib_volume_symbol { get; set; }
    public required string bib_version { get; set; }
    public required string bib_isbn { get; set; }
    public required string bib_issn { get; set; }
    public required string bib_magazine_code { get; set; }
    public int? bib_frequency_id { get; set; }
    public required string bib_book_title { get; set; }
    public required string bib_book_title_yomi { get; set; }
    public required string bib_book_subtitle { get; set; }
    public required string bib_book_subtitle_yomi { get; set; }
    public required string bib_series { get; set; }
    public required string bib_series_yomi { get; set; }
    public required string bib_origin { get; set; }
    public required string bib_author { get; set; }
    public required string bib_author_yomi { get; set; }
    public required string bib_author_intro { get; set; }
    public required string bib_note { get; set; }
    public required string bib_general_subject { get; set; }
    public required string bib_general_subject_yomi { get; set; }
    public required string bib_personal_subject { get; set; }
    public required string bib_personal_subject_yomi { get; set; }
    public required string bib_content_intro { get; set; }
    public required string bib_content_detail { get; set; }
    public required string bib_publisher { get; set; }
    public required string bib_publish_place { get; set; }
    public required string bib_publish_year { get; set; }
    public required string bib_page_nums { get; set; }
    public required string bib_book_size { get; set; }
    public required string bib_price { get; set; }
    public required string bib_mark_id { get; set; }
    public int? bib_mark_category_id { get; set; }
    public required string bib_isbn_secondary { get; set; }
    public int? bib_parent_bib_id { get; set; }
    public int? bib_target_id { get; set; }
    public int? bib_publish_country_code_id { get; set; }
    public required string bib_work_language { get; set; }
    public required string bib_no { get; set; }
    public required string bib_volume_no { get; set; }
    public required string bib_registdate { get; set; }
    public required string bib_lastupdate { get; set; }
    public static string SQL = @"SELECT bib_id,bib_code,bib_interlibrary_import_library_id,bib_interlibrary_import_bib_id,bib_material_id,bib_language_id,bib_alternative_symbol,bib_class_symbol_id,bib_class_symbol_secondary_id,bib_book_symbol,bib_volume_symbol,bib_version,bib_isbn,bib_issn,bib_magazine_code,bib_frequency_id,bib_book_title,bib_book_title_yomi,bib_book_subtitle,bib_book_subtitle_yomi,bib_series,bib_series_yomi,bib_origin,bib_author,bib_author_yomi,bib_author_intro,bib_note,bib_general_subject,bib_general_subject_yomi,bib_personal_subject,bib_personal_subject_yomi,bib_content_intro,bib_content_detail,bib_publisher,bib_publish_place,bib_publish_year,bib_page_nums,bib_book_size,bib_price,bib_mark_id,bib_mark_category_id,bib_isbn_secondary,bib_parent_bib_id,bib_target_id,bib_publish_country_code_id,bib_work_language,bib_no,bib_volume_no,bib_registdate,bib_lastupdate FROM stg_bibliography";
    
    public void SetDataSample(string[] fields)
    {
      bib_id = int.Parse(fields[0]);
      bib_code = fields[1];
      bib_interlibrary_import_library_id = string.IsNullOrEmpty(fields[2]) ? (int?)null : int.Parse(fields[2]);
      bib_interlibrary_import_bib_id = string.IsNullOrEmpty(fields[3]) ? (int?)null : int.Parse(fields[3]);
      bib_material_id = string.IsNullOrEmpty(fields[4]) ? (int?)null : int.Parse(fields[4]);
      bib_language_id = string.IsNullOrEmpty(fields[5]) ? (int?)null : int.Parse(fields[5]);
      bib_alternative_symbol = fields[6];
      bib_class_symbol_id = string.IsNullOrEmpty(fields[7]) ? (int?)null : int.Parse(fields[7]);
      bib_class_symbol_secondary_id = string.IsNullOrEmpty(fields[8]) ? (int?)null : int.Parse(fields[8]);
      bib_book_symbol = fields[9];
      bib_volume_symbol = fields[10];
      bib_version = fields[11];
      bib_isbn = fields[12];
      bib_issn = fields[13];
      bib_magazine_code = fields[14];
      bib_frequency_id = string.IsNullOrEmpty(fields[15]) ? (int?)null : int.Parse(fields[15]);
      bib_book_title = fields[16];
      bib_book_title_yomi = fields[17];
      bib_book_subtitle = fields[18];
      bib_book_subtitle_yomi = fields[19];
      bib_series = fields[20];
      bib_series_yomi = fields[21];
      bib_origin = fields[22];
      bib_author = fields[23];
      bib_author_yomi = fields[24];
      bib_author_intro = fields[25];
      bib_note = fields[26];
      bib_general_subject = fields[27];
      bib_general_subject_yomi = fields[28];
      bib_personal_subject = fields[29];
      bib_personal_subject_yomi = fields[30];
      bib_content_intro = fields[31];
      bib_content_detail = fields[32];
      bib_publisher = fields[33];
      bib_publish_place = fields[34];
      bib_publish_year = fields[35];
      bib_page_nums = fields[36];
      bib_book_size = fields[37];
      bib_price = fields[38];
      bib_mark_id = fields[39];
      bib_mark_category_id = string.IsNullOrEmpty(fields[40]) ? (int?)null : int.Parse(fields[40]);
      bib_isbn_secondary = fields[41];
      bib_parent_bib_id = string.IsNullOrEmpty(fields[42]) ? (int?)null : int.Parse(fields[42]);
      bib_target_id = string.IsNullOrEmpty(fields[43]) ? (int?)null : int.Parse(fields[43]);
      bib_publish_country_code_id = string.IsNullOrEmpty(fields[44]) ? (int?)null : int.Parse(fields[44]);
      bib_work_language = fields[45];
      bib_no = fields[46];
      bib_volume_no = fields[47];
      bib_registdate = fields[48];
      bib_lastupdate = fields[49];
    }
  }
  /// <summary>
  /// stg_collectionより生成されたクラス
  /// </summary>
  internal class stg_collection : TableRow
  {
    public required int collection_id { get; set; }
    public required string collection_book_code { get; set; }
    public int? collection_bib_id { get; set; }
    public required string collection_alternative_symbol { get; set; }
    public int? collection_class_symbol_id { get; set; }
    public int? collection_class_symbol_secondary_id { get; set; }
    public required string collection_book_symbol { get; set; }
    public required string collection_volume_symbol { get; set; }
    public int? collection_mediatype_id { get; set; }
    public int? collection_shelf_id { get; set; }
    public int? collection_forbidden_category_id { get; set; }
    public int? collection_interlibrary_category_id { get; set; }
    public int? collection_interlibrary_import_library_id { get; set; }
    public int? collection_interlibrary_import_collection_id { get; set; }
    public int? collection_status_id { get; set; }
    public int? collection_accept_book_id { get; set; }
    public required int collection_uncertained_count { get; set; }
    public required string collection_remarks { get; set; }
    public required string collection_shelf_date { get; set; }
    public required string collection_registdate { get; set; }
    public required string collection_lastupdate { get; set; }
    public static string SQL = @"SELECT collection_id,collection_book_code,collection_bib_id,collection_alternative_symbol,collection_class_symbol_id,collection_class_symbol_secondary_id,collection_book_symbol,collection_volume_symbol,collection_mediatype_id,collection_shelf_id,collection_forbidden_category_id,collection_interlibrary_category_id,collection_interlibrary_import_library_id,collection_interlibrary_import_collection_id,collection_status_id,collection_accept_book_id,collection_uncertained_count,collection_remarks,collection_shelf_date,collection_registdate,collection_lastupdate FROM stg_collection";
    
    public void SetDataSample(string[] fields)
    {
      collection_id = int.Parse(fields[0]);
      collection_book_code = fields[1];
      collection_bib_id = string.IsNullOrEmpty(fields[2]) ? (int?)null : int.Parse(fields[2]);
      collection_alternative_symbol = fields[3];
      collection_class_symbol_id = string.IsNullOrEmpty(fields[4]) ? (int?)null : int.Parse(fields[4]);
      collection_class_symbol_secondary_id = string.IsNullOrEmpty(fields[5]) ? (int?)null : int.Parse(fields[5]);
      collection_book_symbol = fields[6];
      collection_volume_symbol = fields[7];
      collection_mediatype_id = string.IsNullOrEmpty(fields[8]) ? (int?)null : int.Parse(fields[8]);
      collection_shelf_id = string.IsNullOrEmpty(fields[9]) ? (int?)null : int.Parse(fields[9]);
      collection_forbidden_category_id = string.IsNullOrEmpty(fields[10]) ? (int?)null : int.Parse(fields[10]);
      collection_interlibrary_category_id = string.IsNullOrEmpty(fields[11]) ? (int?)null : int.Parse(fields[11]);
      collection_interlibrary_import_library_id = string.IsNullOrEmpty(fields[12]) ? (int?)null : int.Parse(fields[12]);
      collection_interlibrary_import_collection_id = string.IsNullOrEmpty(fields[13]) ? (int?)null : int.Parse(fields[13]);
      collection_status_id = string.IsNullOrEmpty(fields[14]) ? (int?)null : int.Parse(fields[14]);
      collection_accept_book_id = string.IsNullOrEmpty(fields[15]) ? (int?)null : int.Parse(fields[15]);
      collection_uncertained_count = int.Parse(fields[16]);
      collection_remarks = fields[17];
      collection_shelf_date = fields[18];
      collection_registdate = fields[19];
      collection_lastupdate = fields[20];
    }
  }
}
