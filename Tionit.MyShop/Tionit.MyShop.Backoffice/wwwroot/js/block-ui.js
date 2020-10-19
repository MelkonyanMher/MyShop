/** Блокирует страницу */
window.blockPage = function () {
    KTApp.blockPage({
        overlayColor: '#000000',
        type: 'v2',
        state: 'danger',
        size: 'lg'
    });
}

/** Выполняет разблокировку страницы */
window.unblockPage = function () {
    KTApp.unblockPage();
}

/**
 * Блокирует элемент по селектору
 * @param {string} selector селектор
 */
window.block = function (selector) {
    KTApp.block(selector, {
        overlayColor: '#000000',
        type: 'v2',
        state: 'danger',
        size: 'lg'
    });
}

/**
 * Выполняет разблокировку элемента по селектору
 * @param {string} selector селектор
 */
window.unblock = function (selector) {
    KTApp.unblock(selector);
}