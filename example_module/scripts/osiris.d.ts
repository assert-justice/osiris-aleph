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
        }
        function listActors(): Actor[];
        function getActor(id: string): Actor | undefined;
        // function setEventHandler(fn: (actor: Actor, event: object) => void): void;
    }
    export namespace AssetLog{
        function addFile(filename: string): void;
        function removeFile(filename: string): boolean;
        function listFiles(): string[];
    }
    export namespace Blob{
        class Blob{
            constructor(groupName?: string): void;
            applyEvent(event: object): void;
            setEventHandler(fn: (event: object) => void): void;
        }
        function setGroupEventHandler(groupName?: string, fn: (event: object) => void): void;
    }
    export namespace Map{
        type BlockerStatus = "wall" | "open" | "closed" | "locked";
        class Blocker{
            getStatus(): BlockerStatus;
            setStatus(status: BlockerStatus): void;
            getStart(): [number, number];
            setStart(start: [number, number]): void;
            getEnd(): [number, number];
            setEnd(end: [number, number]): void;
            isOpaque(): boolean;
            setOpaque(value: boolean): void;
            isSolid(): boolean;
            setSolid(value: boolean): void;
            applyEvent(event: object): void;
        }
        class Layer{
            getName(): string;
            setName(name: string): void;
            isVisible(): boolean;
            setIsVisible(value: boolean): void;
            getStamps(): Stamp[];
            setStamps(stamps: Stamp[]): void;
            applyEvent(event: object): void;
        }
        class Map{
            applyEvent(event: object): void;
        }
        class Stamp{
            getImage(): StampImage | undefined;
            getText(): StampImage | undefined;
            getStats(): object | undefined;
            applyEvent(event: object): void;
        }
        function getCurrentMap(): Map;
        function listMaps(): Map[];
        function setEventHandler(fn: (map: Map, event: object) => void): void;
    }
    export namespace System{
        function emitEvent(event: object, isPublic: boolean = false): void;
        function isGm(): boolean;
        function isPlayer(): boolean;
    }
}
