import { ListItem } from "../data/list-item";

export class LocalizablesDTO extends ListItem {
    langId: number = 0;
    data: { [key: string]: any } = {};
}