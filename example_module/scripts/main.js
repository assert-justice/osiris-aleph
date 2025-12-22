import { Logging, Actor } from "Osiris";
// import { validateActors } from "validate_actors";

export function init(){
    Logging.log("Module initialized");
    // validateActors();
    // Event.setEventHandler("actor", (s, obj)=>{Logging.log(s, ": ", Object.entries(obj))});
    // Event.dispatchEvent(Actor.listActors()[0].Id, "actor", {yo:"howdy"});
    let actor = Actor.listActors()[0];
    actor.Id = "butts";
    Logging.log("id: ", actor.Id);
    actor = Actor.listActors()[0];
    Logging.log("id: ", actor.Id);
}
