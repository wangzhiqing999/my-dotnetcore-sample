// Dom7
var $$ = Dom7;

// Framework7 App main instance
var app  = new Framework7({
  root: '#app', // App root element
  id: 'io.framework7.testapp', // App bundle ID
  name: 'Framework7', // App name
  theme: 'auto', // Automatic theme detection
  // App root data
  data: function () {
    return {
      user: {
        firstName: 'John',
        lastName: 'Doe',
      },
    };
  },
  // App root methods
  methods: {
    helloWorld: function () {
      app.dialog.alert('Hello World!');
    },
  },
  // App routes
  routes: routes,
  // Enable panel left visibility breakpoint
  panel: {
    leftBreakpoint: 960,
  },
});

// Init/Create left panel view
var mainView = app.views.create('.view-left', {
  url: '/'
});

// Init/Create main view
var mainView = app.views.create('.view-main', {
  url: '/'
});





// Login Screen
$$('#my-login-screen .login-button').on('click', function () {

  myAuthentication.orgCode = $$('#my-login-screen [name="orgCode"]').val();
  myAuthentication.username = $$('#my-login-screen [name="username"]').val();
  myAuthentication.password = $$('#my-login-screen [name="password"]').val();

  myAuthentication.doLogin(doLoginSuccess);
});

function doLoginSuccess() {
  // Close login screen
  app.loginScreen.close('#my-login-screen');
}

// Log Off.
$$('#logOff').on('click', function () {
	app.router.navigate("/");
	var loginScreen = app.loginScreen.create({ el : $$("#my-login-screen") });
	loginScreen.open(true);
});



