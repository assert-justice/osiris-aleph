declare module "Osiris"{
    export namespace Logging{
        function log(...args: any[]): void;
        function logError(...args: any[]): void;
    }
    export namespace Actor{
        function listActors(): string[];
    }
}
