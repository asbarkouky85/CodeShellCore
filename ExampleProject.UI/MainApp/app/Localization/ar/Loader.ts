﻿import { LocaleLoader } from "codeshell/lib/localization/localeLoader";

import Columns from "./Columns.json";
import Messages from "./Messages.json";
import Pages from "./Pages.json";
import Words from "./Words.json";

export class ar_Loader extends LocaleLoader {

    public Load(): any {

        return {
            Messages: Messages,
            Pages: Pages,
            Words: Words,
            Columns: Columns
        };
    }
}