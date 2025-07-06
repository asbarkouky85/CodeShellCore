import { ListItem } from "./listItem";

export class LocalizablesDTO extends ListItem {
    langId: number = 0;
    data: { [key: string]: any } = {};
}