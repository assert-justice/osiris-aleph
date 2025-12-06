# Osiris VTT

An open source VTT powered by the Godot game engine. Supports a wide variety of game systems and plugins with a rich Python based scripting system.

## User Settings

The user settings are global to all the user's games.

main_volume: The main audio volume, summing all the rest.
music_volume: The volume of music players.
ambiance_volume: The volume of ambiance players.
sfx_volume: The volume of sound effects.
font_override?: If present and not null, reference one of the fonts in the user's fonts directory. Should by default include accessible fonts for users with dyslexia etc.
use_high_contrast_filter?: Enable a high contrast filter.
debug_color_blind_filter?: One of several screen space filters that simulate colorblindness. To be used by gms and module authors to help identify accessibility problems related to vision. Valid values are monochromacy, protanopia, deuteranopia, and tritanopia.
map_background_color_override?: Override the background color of all maps.
map_border_color_override?: Override the border color of all maps.
grid_color_override?: Override the grid color of all maps.
grid_width_override?: Override the grid width of all maps.
disable_particles?: If present and true this setting disables particle effects.
disable_animated_sprites?: If present and true this setting disables animated sprites.
disable_animations?: If present and true this setting disables animations, skipping to the end immediately.
disable_screen_effects?: If present and true this setting disables screen effects like screen shake and filters.
animation_speed?: A coefficient applied to all animation speeds. For example if this field is set to 2 all animations will play at double speed. Defaults to 1.
