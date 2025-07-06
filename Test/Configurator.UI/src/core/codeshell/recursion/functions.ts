
import { TreeNode } from '@circlon/angular-tree-component';
import { RecursionModel } from './recursion-model';

export function List_RunRecursively_Nodes(lst: TreeNode[], func: (m: TreeNode) => void) {
    for (let mod of lst) {
        func(mod);
        if (mod.children && mod.children.length > 0)
            List_RunRecursively_Nodes(mod.children, func);
    }
}

export function List_FindIdRecusive(lst: RecursionModel[], id: number): RecursionModel | null {
    for (let mod of lst) {
        if (mod.id == id) {
            return mod;
        } else if (mod.children && mod.children.length > 0) {
            var m = List_FindIdRecusive(mod.children, id);
            if (m)
                return m;
        }
    }
    return null;
}

export function List_RunRecursively(lst: RecursionModel[], func: (m: RecursionModel) => void) {
    for (let mod of lst) {
        func(mod);
        if (mod.children && mod.children.length > 0)
            List_RunRecursively(mod.children, func);
    }
}

export function List_RunRecursively_GEN<T>(lst: T[], func: (m: T) => void) {

    for (let mod of lst) {
        func(mod);
        if ((mod as any).children && (mod as any).children.length > 0)
            List_RunRecursively_GEN<T>((mod as any).children, func);
    }
}

export function List_RunRecursivelyUp_GEN<T>(mod: T, func: (m: T) => void, first: boolean = true) {
    if (!first)
        func(mod)
    let Par = (mod as any).parent;

    if (Par)
        List_RunRecursivelyUp_GEN<T>(Par, func, false);

}