/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    // extend: {
    //   keyframes: {
    //     loading: {
    //       '0%': { bottom: '-100%' },
    //       '50%': { bottom: '-80%' },
    //       '100%': { bottom: '0' }
    //     }
    //   },
    //   animation: {
    //     loading: 'loading .5s linear '
    //   }
    // }
  },
  fontFamily: {
    'body': [
      'Open Sans', 
      'ui-sans-serif', 
      'system-ui',
      // other fallback fonts
    ],
    'sans': [
      'Inter', 
      'ui-sans-serif', 
      'system-ui',
      // other fallback fonts
    ]
  },
  colors: {
    "text" : '3b3951'
  },
  plugins: []
}
/*
    
  


*/
