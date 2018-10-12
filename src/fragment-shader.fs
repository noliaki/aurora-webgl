precision mediump float;
uniform float u_time; // time
uniform vec2  u_resolution; // resolution
uniform vec2 u_mouse;
uniform vec2 c_mouse;

void main(void){
  vec2 p = (gl_FragCoord.xy * 2.0 - u_resolution) / min(u_resolution.x, u_resolution.y);
  vec2 p_mouse = (u_mouse.xy * 2.0 - u_resolution) / min(u_resolution.x, u_resolution.y);

  float noise = (snoise(vec3(1.0, 1.0, u_time / 3.0)) + 1.0) / 2.0;

  float offset = (1.0 / length(vec2(p.x - p_mouse.x, p.y + p_mouse.y))) * noise / 2.0;

  vec3 rgb_color = vec3(
    (snoise(vec3(p.x / 3.0, p.y / 3.0, u_time / 1.1)) + 1.0) / 2.0 + 0.2 + offset,
    (snoise(vec3(p.x / 2.0, p.y / 2.0, u_time / 1.2)) + 1.0) / 2.0 + 0.1 + offset,
    (snoise(vec3(p.x / 5.0, p.y / 5.0, u_time / 1.3)) + 1.0) / 2.0 + offset
  );

  gl_FragColor = vec4(rgb_color, 1.0);
}
