function displayBusyIndicator() {
    $('.loading').show();
}

$(document).ready(function () {
    $(document).on('submit', 'form',
        function () {
            if ($(document.activeElement).attr('id') == 'buttonBusyIndicator' || $(this).attr('id') == 'buttonBusyIndicator') {
                displayBusyIndicator();
            }
        });
});