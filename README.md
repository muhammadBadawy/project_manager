# The Project manager

This project is a web application made with ASP.net

A projects management system is a web application that allows customers that have projects to communicate and hire project managers and Team leaders to accomplish and deliver their projects in an easy and fast way. 
 
Actors (Five actors): 
- Admin 
- Customers 
- Project Managers (PMs) 
- Team Leaders (TLs) 
- Junior Developers (JDs) 

Roles: 
1- Admin:  
  a. Control Users (Customers, PM, TL, JD): Add, Remove Users. 
  b. Control the Home Profile: Add, Remove, Update Posts (Means Projects) created by the customers. 
  c. Admin page layout (profile) includes: 
    i. Admin information: Photo, Job description, FirstName, LastName, Mobile, Email.
    ii. All the actors that using the system so that the admin can control them. 
    iii. All the posts that created by the customers so that the admin can control them. 
    iv. If the Admin remove a post from his profile It reflect directly to the home page and be removed. 

2- Project Manager: 
  a. Surfing the projects created by the Customers in the home page and selecting the appropriate projects then starting to create the team to achieve and deliver the projects. 
  b. Project Manager layout (profile) includes:  
    i. PM information: Photo, Job description, FirstName, LastName, Mobile, Email. 
    ii. PM Statistical diagrams (Pie Chart or Histogram) that shows his Qualifications and experience. 
    iii. All the Current projects that he managing it. 
    iv. All the previous projects that he delivered it (history). 
    v. Creating teams Module:  
      1. Search for team leaders and sending requests to make them join the team. 
      2. Search for junior developers and sending requests to make them join the team. 
    vi. Control Projects Module:  
      1. Set Schedule for each project. 
      2. Set the status for each project (on progress, Delivered). 
      3. Write Comments, set Price for each project. 
      4. Remove Members (remove Undesired member). 
      5. Remove Projects (leave the project). 

3- Team Leader:   
  a. Checking His Profile to see the requests that have been sent by any project manager that need him to join the team and send response either accept or reject. 
  b. Team Leader layout (profile) includes:  
    i. TL information: Photo, Job description, FirstName, LastName, Mobile, Email. 
    ii. TL Statistical diagrams (Pie Chart or Histogram) that shows his Qualifications and experience. 
    iii. All the Current projects that he currently joining it. 
    iv. All the previous projects that he delivered it before (history). 
    v. Creating teams Module:  
      1. Send Requests to JDs to Help the PM to Search for junior developers and sending requests to make them join the team. 
    vi. Evaluate Junior Developers Module:  
      1. Evaluate JDs and send feedback to the PM. 
      2. Remove JD (remove Undesired member) but PM must approve. 
      3. Remove Projects (leave the project) but PM must approve. 

4- Junior Developers:  
  a. Checking His Profile to see the requests that have been sent by any project managers or Team leaders that need him to join the team and send response either accept or reject. 
  b. Junior Developers layout (profile) includes:  
    i. JD information: Photo, Job description, FirstName, LastName, Mobile, Email. 
    ii. JD Statistical diagrams (Pie Chart or Histogram) that shows his Qualifications and experience. 
    iii. All the Current projects that he currently joining it. 
    iv. All the previous projects that he delivered it before (history). 
    v. Control Projects Module:  
      1. Remove Projects (leave the project) but PM must approve. 

5- Customers:  
  a. Creating projects. 
  b. Customer layout (profile) includes:  
    i. Customer information: Photo, description, FirstName, LastName, Mobile, Email. 
    ii. All the Current projects that not delivered yet. 
    iii. All the previous projects that have been delivered to him by the PM. 
    iv. Control Projects Module:  
      1. Remove Projects: remove the projects but if it has been assigned to PM then it cannot be removed. 
      2. Add Projects. 
      3. Assign Projects to PMs. 
      4. Edit Projects. 
