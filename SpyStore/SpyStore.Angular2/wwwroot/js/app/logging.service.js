"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class LoggingService {
    constructor() { }
    logError(message, err) {
        this.showPopup(message);
        console.log(err);
    }
    showPopup(message) {
        alert(message);
    }
    log(message) {
        console.log(message);
    }
}
exports.LoggingService = LoggingService;
//# sourceMappingURL=logging.service.js.map