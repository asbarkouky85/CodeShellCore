import { TreeNode } from "angular-tree-component";

export class TreeEventArgs {
    

    constructor(public EventName:string, public Node: TreeNode) { }
}