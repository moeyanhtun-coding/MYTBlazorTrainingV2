export function showModal(id) {
    const modal = new bootstrap.Modal(document.querySelector(id));
    modal.show();
}

export function hideModal(id) {
    const modalEl = document.querySelector(id);
    const modal = bootstrap.Modal.getInstance(modalEl);
    modal.hide();
}
