/// <reference path="../ckfinder/ckfinder.html" />
/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.enterMode = CKEDITOR.ENTER_BR;

    config.language = 'vi';
    config.filebrowserBrowseUrl = '/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/plugins/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Uploads/Files';
    config.filebrowserFlashUploadUrl = '/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    config.toolbarGroups = [
         { name: 'document', groups: ['mode', 'document', 'doctools'] },
         { name: 'clipboard', groups: ['clipboard', 'undo'] },
         { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
         { name: 'forms', groups: ['forms'] },
         
         { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
         { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
         { name: 'links', groups: ['links'] },
         { name: 'insert', groups: ['insert'] },
         '/',
         { name: 'styles', groups: ['styles'] },
         { name: 'colors', groups: ['colors'] },
         { name: 'tools', groups: ['tools'] },
         { name: 'others', groups: ['others'] },
         { name: 'about', groups: ['about'] }
    ];

    config.removeButtons = 'ImageButton,Button,Select,Textarea,TextField,Radio,Checkbox,Form,HiddenField,Save,NewPage,Print,Flash,About';
};
