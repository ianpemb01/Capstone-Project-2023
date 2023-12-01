# Capstone-Project-2023

Ian Pemberton Capstone Project Plan

I received a degree in ceramic Fine Arts and would like to create an app that I could use during the process of creating molds. Plaster is the physical substance used to create a mold and can’t be reused unless dried and crushed into a powder form. Once water is added to the dry plaster, a chemical process occurs causing the plaster to change causing a rejection of any further addition of plaster. Also, there is a short window in which the plaster remains in a liquid form. With slip casting, a certain amount of plaster must surround the cavity inside the plaster mold so as to create an even absorption rate between all parts of the object being cast.
Without going into too much detail about ceramic processes, I would like to create an app for calculating the amount of plaster needed to create a mold.  This will require user input, coding several classes for the general object of the shape, and a data bank so that the user can come back to the project at a later date. Most shapes fall into the form of spheres, pyramids, cylinders, or cubes. The app will offer to calculate the amount of plaster and water based on the original object's 3 dimensional measurements. 
The volume of a rectangular shape equals its length multiplied by its width multiplied by its height. Divide the resulting volume by eighty to find the number of quarts necessary to mix enough plaster. To determine the weight of plaster needed, multiply the volume of water by 3. This will give the pounds of plaster needed to mix. This is an example of the programming will do for each shape type.
I created a generic class directly under the namespace in MyDbContext, lines 15 through 18:
    public List<string?> GetEntities<T>(string propertyName) where T : class
    {
        return [.. Set<T>().Select(entity => entity.GetType().GetProperty(propertyName)!.GetValue(entity)!.ToString()).ToList()];
    }
and used it at lines 228 through 242. This was a general query to return a list of Project names from the SQLite server.
	I am following two of the SOLID principles, specifically SRP and OCP. My classes are clear and only deal in simple instructions. With OCP principle, by creating the possibility of further use of the Cube, Cylinder, Sphere, and Pyramid classes.
I've used async to create a save changes method, taking user input and sending it to a database.




