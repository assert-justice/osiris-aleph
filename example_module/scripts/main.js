import { Logging } from "Osiris";
import { validateActors } from "validate_actors";

export function init(){
    Logging.log("Module initialized");
    validateActors();
}
