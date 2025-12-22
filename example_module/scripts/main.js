import { Logging } from "Osiris";
import { validateActors } from "validate_actors";

export function init(){
    Logging.Log("Module initialized");
    validateActors();
}
