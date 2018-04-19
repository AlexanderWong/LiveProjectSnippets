        [HttpGet]
        public ActionResult Edit(string Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser dataUser = new ApplicationUser();

            dataUser = db.Users.Where(x => x.Id == Id).First();
            
            RegisterViewModel rv = new RegisterViewModel();
            rv.PhoneNumber = dataUser.PhoneNumber;
            rv.FirstName = dataUser.FirstName;
            rv.LastName = dataUser.LastName;
            rv.UserName = dataUser.UserName;
            rv.Department = dataUser.Department;
            rv.Position = dataUser.Position;
            rv.Address = dataUser.Address;
            rv.HourlyPayRate = dataUser.HourlyPayRate;
            rv.HireDate = dataUser.HireDate;
            rv.Email = dataUser.Email;
            if (dataUser == null)
            {
                return HttpNotFound();
            }
            return View("Edit", rv);
            
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPost(string Id)
        {
            var userToUpdate = db.Users.Where(x => x.Id == Id).First();
            
            if (TryUpdateModel(userToUpdate, "", new string[] {"FirstName", "LastName", "UserName", "PhoneNumber", "Department", "Position", "Address", "HourlyPayRate", "Email", "HireDate", "Fulltime"}))
            {
                try
                {
                    db.SaveChanges();
                    return View("~/Views/Home/Index.cshtml");
                }
                catch (DataException)
                {

                    ModelState.AddModelError("","Unable to save changes");
                }
            }
            return View(userToUpdate);
        }