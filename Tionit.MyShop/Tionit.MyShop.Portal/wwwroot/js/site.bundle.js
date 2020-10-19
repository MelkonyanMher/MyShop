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
window.animateContent = function(){
    var animation = "animate-fade-in-up";
    $("#kt_content")
        .one("webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend",
            e => $("#kt_content").removeClass(animation))
        .removeClass(animation).addClass(animation);
}
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function () { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function () { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};

var Messages = /** @class */ (function () {
    function Messages() {
    }
    /**
  * Показывает всплывающее сообщение об ошибке
  * @param message Текст сообщения
  */
    Messages.showError = function (message) {
        toastr.options.positionClass = "toast-bottom-right";
        toastr.options.showMethod = "slideDown";
        toastr.error(message);
    };
    /**
     * Показывает сообщение об ошибке в модальном окне
     * @param message Текст сообщения
     */
    Messages.showErrorModal = function (message) {
        swal.fire({
            text: message,
            type: "error"
        });
    };
    /**
     * Показывает всплывающее сообщение с предупреждением
     * @param message Текст сообщения
     */
    Messages.showWarning = function (message) {
        toastr.warning(message);
    };
    /**
     * Показывает сообщение с предупреждением в модальном окне
     * @param message Текст сообщения
     */
    Messages.showWarningModal = function (message) {
        swal.fire({
            text: message,
            type: "warning"
        });
    };
    /**
     * Показывает всплывающее сообщение с информацией
     * @param message Текст сообщения
     */
    Messages.showInfo = function (message) {
        toastr.options.positionClass = "toast-bottom-right";
        toastr.options.showMethod = "slideDown";
        toastr.info(message);
    };
    /**
     * Показывает сообщение с информацией в модальном окне
     * @param message Текст сообщения
     */
    Messages.showInfoModal = function (message) {
        swal.fire({
            text: message,
            type: "info"
        });
    };
    /**
     * Показывает всплывающее сообщение об успехе
     * @param message Текст сообщения
     */
    Messages.showSuccess = function (message) {
        toastr.options.positionClass = "toast-bottom-right";
        toastr.options.showMethod = "slideDown";
        toastr.success(message);
    };
    /**
     * Показывает сообщение об ошибке в модальном окне
     * @param message Текст сообщения
     */
    Messages.showSuccessModal = function (message) {
        swal.fire({
            text: message,
            type: "success"
        });
    };
    /**
     * Отображает конфирм
     */
    Messages.showConfirm = function (message) {
        return __awaiter(this, void 0, Promise, function () {
            var swalPromise;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        swalPromise = swal.fire({
                            text: message,
                            type: "question",
                            showCancelButton: true,
                            confirmButtonColor: "#22b794",
                            cancelButtonColor: "#d33",
                            confirmButtonText: "Да",
                            cancelButtonText: "Нет",
                            buttonsStyling: true
                        });
                        return [4 /*yield*/, new Promise(function (resolve) { return swalPromise.then(function (result) { return resolve(result.value != undefined); }); })];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    return Messages;
}());