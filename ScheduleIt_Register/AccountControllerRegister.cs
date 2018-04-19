// GET: /Account/Register
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")] //adds authorization so only the admin can register new users.
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Department = model.Department,
                    Position = model.Position,
                    Address = model.Address,
                    HireDate = model.HireDate,
                    HourlyPayRate = model.HourlyPayRate,
                    Fulltime = model.Fulltime
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //load index of users after user is registered.
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View();
        }