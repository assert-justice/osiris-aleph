import { Logging, Actor } from "Osiris";

Logging.log("Validate actor module loaded!");

export function validateActors(){
    Logging.log("validating...");
    for (const actor of Actor.listActors()) {
        for (const [key, value] of Object.entries(actor)) {
            Logging.log(key, ": ", value);
        }
    }
}
