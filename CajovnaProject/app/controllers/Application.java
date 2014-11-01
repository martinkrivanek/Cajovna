package controllers;

import java.sql.Connection;
import java.sql.SQLException;

import javax.sql.DataSource;

import play.*;
import play.mvc.*;
import play.db.*; 
import views.html.*;

public class Application extends Controller {

    public static Result index() {
    	
    	Connection connection = DB.getConnection();
    	
    	try {
			return ok(index.render("Your new application is ready. " + connection.getCatalog()));
		} catch (SQLException e) {
			e.printStackTrace();
			return ok(index.render("Your new application is not ready.")); 
		}
    }

}
