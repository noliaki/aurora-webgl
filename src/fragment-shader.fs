precision mediump float;
uniform float u_time; // time
uniform vec2  u_resolution; // resolution
uniform vec2 u_mouse;
uniform vec2 c_mouse;
uniform vec3 u_rgbOffset;
uniform vec3 u_division;
uniform vec3 u_timeRatio;

void main(void){
  vec2 p = (gl_FragCoord.xy * 2.0 - u_resolution) / min(u_resolution.x, u_resolution.y);
  vec2 p_mouse = (u_mouse.xy * 2.0 - u_resolution) / min(u_resolution.x, u_resolution.y);

  float noise = (snoise(vec3(1.0, 1.0, u_time / 3.0)) + 1.0) / 2.0;

  float offset = (1.0 / length(vec2(p.x - p_mouse.x, p.y + p_mouse.y))) * noise / 2.0;

  vec3 rgb_color = vec3(
    (snoise(vec3(p.x / u_division.x, p.y / u_division.x, u_time / u_timeRatio.x)) + 1.0) / 2.0 + u_rgbOffset.x + offset,
    (snoise(vec3(p.x / u_division.y, p.y / u_division.y, u_time / u_timeRatio.y)) + 1.0) / 2.0 + u_rgbOffset.y + offset,
    (snoise(vec3(p.x / u_division.z, p.y / u_division.z, u_time / u_timeRatio.z)) + 1.0) / 2.0 + u_rgbOffset.z + offset
  );

  gl_FragColor = vec4(rgb_color, 1.0);
}
