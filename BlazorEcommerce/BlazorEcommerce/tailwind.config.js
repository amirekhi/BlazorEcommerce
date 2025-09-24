/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "../BlazorEcommerce.Client/**/*.razor",   // ✅ Razor components in client project
    "../BlazorEcommerce.Client/**/*.html",    // if you use raw HTML templates
    "../BlazorEcommerce.Client/**/*.cshtml",  // unlikely, but just in case
    "../BlazorEcommerce.Client/**/*.js",      // any client-side JS you want to scan
    "./**/*.cshtml",                          // server-side fallback (optional)
    "./**/*.razor",                           // Server-side razor files (if any)
  ],
  safelist: [
    'hide-scrollbar'
  ],
  theme: {
    extend: {},
  },
  plugins: [require('daisyui')],
}
