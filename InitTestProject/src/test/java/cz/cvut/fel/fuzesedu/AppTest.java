package cz.cvut.fel.fuzesedu;

import cz.cvut.fel.fuzesedu.user.DBUser;
import org.junit.Test;
import org.hibernate.exception.ConstraintViolationException;

import junit.framework.TestCase;

/**
 * Unit test for simple App.
 */
public class AppTest extends TestCase {
	@Test
	public void testApp() {
		assertTrue(true);
	}

	@Test(expected = ConstraintViolationException.class)
	public void testHappy() {
		AppBetter appBetter = new AppBetter();
		appBetter.add();
	}

	@Test
	public void testEvil() {
		AppBetter appBetter = new AppBetter();
		DBUser user = appBetter.get();
		assertEquals("Hibernate101", user.getUsername());
	}
}
