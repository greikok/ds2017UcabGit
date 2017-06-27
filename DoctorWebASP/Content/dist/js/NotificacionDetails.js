
CKEDITOR.replace('NotificacionContenido', {
    // Define the toolbar groups as it is a more accessible solution.
    toolbarGroups: [
        { name: 'clipboard', groups: ['clipboard', 'undo'] },
        { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
        { name: 'links', groups: ['links'] },
        { name: 'insert', groups: ['insert'] },
        { name: 'forms', groups: ['forms'] },
        { name: 'tools', groups: ['tools'] },
        { name: 'others', groups: ['others'] },
        '/',
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
        { name: 'styles', groups: ['styles'] },
        { name: 'colors', groups: ['colors'] },
        { name: 'about', groups: ['about'] }
    ],
    extraPlugins: 'divarea',
    change: function (evt) {
        // getData() returns CKEditor's HTML content.
        console.log('Total bytes: ' + evt.editor.getData().length);
    }
    // Remove the redundant buttons from toolbar groups defined above.
    //removeButtons: 'Underline,Strike,Subscript,Superscript,Anchor,Styles,Specialchar'
});

CKEDITOR.on('instanceReady', function () {
    $("#" + CKEDITOR.instances.NotificacionContenido.id + "_contents > div").taggedText();
    //var superUpdateElement = CKEDITOR.instances.NotificacionContenido.updateElement;
    //CKEDITOR.instances.NotificacionContenido.updateElement = function () {
    //    var input = CKEDITOR.document.getById('NotificacionContenido');
    //    input.setHtml($(taggetTextID).taggedText({ method: "getContentWithKeys" })[0]);
    //    var result = superUpdateElement();
    //    input.setHtml($(taggetTextID).taggedText({ method: "getContentWithKeys" })[0]);
    //    console.log("updateElement");
    //    return result;
    //};
});

$(document).ready(function () {
    $("#FormNotificacion").on("submit", function (event) {
        //event.preventDefault();
        $("#Contenido").val($("#" + CKEDITOR.instances.NotificacionContenido.id + "_contents > div").taggedText({ method: "getContentWithKeys" })[0]);
    });
});


