import { QuillModules } from "ngx-quill";

export class QuillConfig {
    public static Basic: QuillModules =
        {
            toolbar: [
                ['bold', 'italic', 'underline', 'strike'],
                [{ 'direction': 'rtl' }],
                [{ 'align': [] }],
                [{ 'indent': '-1' }, { 'indent': '+1' }],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            ]
        };
}
