import { Culture } from "./localization/locale-data";

export * from "./serverConfigBase";
export * from "./moldster/registry";
export * from "./utilities/utils";
export * from "./shell"
export * from "./codeshell.module";

export function initializeShell() {

    let currentLanguage = Culture.Current.Language;

    let q = document.querySelectorAll("link[data-lang]");
    for (let i = 0; i < q.length; i++) {
        let lnk = q.item(i);
        let val = lnk.getAttribute("data-lang");
        if (val != currentLanguage) {
            lnk.remove();
        }
    }
}