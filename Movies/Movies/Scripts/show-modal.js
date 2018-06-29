function showModal(modalSelector, formTitle) {
    $(modalSelector).modal('show');
    $('.modal-title').html(formTitle);
    $.validator.unobtrusive.parse('#add-data-form');
}

$('#btn-submit-form').on('click', () => {
    const $form = $('#add-data-form');
    $form.submit();
})