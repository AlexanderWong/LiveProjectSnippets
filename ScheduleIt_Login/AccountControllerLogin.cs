        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false); 
            switch (result)
            {
                case SignInStatus.Success:
                return RedirectToAction("LoginRoute");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        [HttpGet]     
        public ActionResult LoginRoute() //Separation of concerns. Redirects the User/Admin to proper view when logging in.
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index");
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
            
         } 