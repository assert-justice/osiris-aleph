declare module "Osiris"{
    export namespace Logging{
        function log(...args: any[]): void;
        function logError(...args: any[]): void;
    }
    export namespace Actor{
        class Actor{
            getId(): string;
            getName(): string;
            setName(name: string): void;
            getPortraitFilename(): string;
            setPortraitFilename(name: string): void;
            getTokenFilename(): string;
            setTokenFilename(name: string): void;
            getStats(): object;
            setStats(state: object): void;
            getState(key: string): string;
            setState(key: string, value: string): void;
            applyEvent(event: object): void;
        }
        function listActors(): Actor[];
        function getActor(id: string): Actor;
        function setEventHandler(fn: (actor: Actor, event: object) => void): void;
    }
}
