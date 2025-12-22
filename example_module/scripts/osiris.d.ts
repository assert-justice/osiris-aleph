declare module "Osiris"{
    export namespace Logging{
        function Log(...args: any[]);
        function LogError(...args: any[]);
    }
    export namespace Actor{
        function ListActors(): string[];
    }
}
