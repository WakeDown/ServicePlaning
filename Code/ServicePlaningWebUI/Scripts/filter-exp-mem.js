//Required 
//sessionstorage.1.4.js 
//jQuery 2.1.0

function initFilterExpandMemmory(filterHeadId, filterPanelId) {

    filterExpMemSesStorId = 'filterExpandMemmoryStorage';

    $(function () {
        var expand = sessionStorage.getItem(filterExpMemSesStorId);
        //alert(expand);
        $('#' + filterPanelId).removeClass('in');
        if (expand == 'true') {
            $('#' + filterPanelId).addClass('in');
                }
    });

    $('#' + filterHeadId).on('click', function () {
        //alert(sessionStorage.getItem(filterExpMemSesStorId));
        var remove = false;
        if (!$('#' + filterPanelId).hasClass('in')) {
            remove = true;
        }
        sessionStorage.setItem(filterExpMemSesStorId, remove);
    });
}