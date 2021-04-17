export abstract class TranslationService {
    abstract setDefaultLang(loc:string): void;
    abstract use(loc:string): void;
}