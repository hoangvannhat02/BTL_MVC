/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = "/Areas/Admin/Data/assets/Plugin/ckfinder/ckfinder.html";
    config.filebrowserImageUrl = "/Areas/Admin/Data/assets/Plugin/ckfinder/ckfinder.html?type=Images";
   
    config.filebrowserUploadUrl = "/Areas/Admin/Data/assets/Plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserFlashUrl = "/Areas/Admin/Data/assets/Plugin/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserImageUploadUrl = "/Areas/Admin/Data/assets/Plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/Areas/Admin/Data/assets/Plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";

   /* config.extraPlugins = 'youtube';*/
    config.language = 'vi';
  /*  config.youtube_responsive = true;*/
};
