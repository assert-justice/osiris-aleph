import { Logging, Actor } from "Osiris";

export function init(){
    Actor.setEventHandler((actor, event)=>{
        Logging.log("here");
        if(event.name) actor.setName(event.name);
    });
    let actor = Actor.listActors()[0];
    Logging.log(Actor.Actor);
    Logging.log("id: ", actor.getId());
    Logging.log("hmn: ", actor.getState["sup"] ?? "yo");
    actor.applyEvent({name: "butts"});
    Logging.log("name: ", actor.getName());
}
