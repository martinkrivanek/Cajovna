package cz.cvut.fel;

import java.util.Date;

import org.hibernate.Session;

import cz.cvut.fel.util.HibernateUtil;

public class App {
	public static void main(String[] args) {
		System.out.println("Maven + Hibernate Annotation + Oracle");
		Session session = HibernateUtil.getSessionFactory().openSession();

		session.beginTransaction();
		DBUser user = new DBUser();

		user.setUserId(100);
		user.setUsername("Hibernate101");
		user.setCreatedBy("system");
		user.setCreatedDate(new Date());

		session.save(user);
		session.getTransaction().commit();
		
		session.beginTransaction();
		DBUser user2 =  (DBUser) session.get(DBUser.class, 100);
		System.out.println(">> " + user2.getUsername());
		session.getTransaction().commit();
	}
}