import { ListItem } from "./ListItem";

export class LocalizablesDTO extends ListItem {
    langId: number = 0;
    data: { [key: string]: any } = {};
}