declare module "Osiris"{
    export namespace Logging{
        /**
         * Converts the arguments to strings, concatenates them, and sends them to the console.
         * @param args - The list of arguments.
         */
        function log(...args: any[]): void;
        /**
         * Converts the arguments to strings, concatenates them, and sends them to be logged as errors.
         * @param args - The list of arguments.
         */
        function logError(...args: any[]): void;
    }
    export namespace Actor{
        /**
         * An actor object.
         */
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
        /**
         * List all actors that are currently loaded.
         * @returns {Actor[]} Returns the array of loaded actor objects.
         */
        function listActors(): Actor[];
        function getActor(id: string): Actor;
        function setEventHandler(fn: (actor: Actor, event: object) => void): void;
    }
    // export namespace Event{
    //     /**
    //      * The type of the target of an event.
    //      */
    //     type TargetType = "actor" | "asset_log" | "blocker" | "handout" | "layer" | "map" | "user";
    //     /**
    //      * Dispatches an event.
    //      * @param {string} targetId - The id of the event target.
    //      * @param {TargetType} targetType - The type of the event target.
    //      * @param {any} payload - The data to send to the target
    //      * @returns {bool} Returns true if it found a valid target, false otherwise.
    //      */
    //     function dispatchEvent(targetId: string, targetType: TargetType, payload: object): bool;
    //     /**
    //      * Associates a target type with an event handler function. Can only be used within 'init' function.
    //      * @param {TargetType} targetType - The type of the event target.
    //      * @param {function(targetId: string, payload: object): void} fn - The function to use as the event handler.
    //      */
    //     function setEventHandler(targetType: TargetType, fn: (targetId: string, payload: object)=>void): void;
    // }
}
