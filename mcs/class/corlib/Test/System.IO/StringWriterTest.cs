//
// System.IO.StringWriter
//
// Author: Marcin Szczepanski (marcins@zipworld.com.au)
//
// TODO: Add some testing for exceptions
//

using NUnit.Framework;
using System.IO;
using System;
using System.Text;

namespace MonoTests.System.IO {

public class StringWriterTest : TestCase {
	public void TestConstructors() {
                StringBuilder sb = new StringBuilder("Test String");

                StringWriter writer = new StringWriter( sb );
                AssertEquals( sb, writer.GetStringBuilder() );
        }

        public void TestWrite() {
                StringWriter writer = new StringWriter();

                AssertEquals( String.Empty, writer.ToString() );
                
                writer.Write( 'A' );
                AssertEquals( "A", writer.ToString() );

                writer.Write( " foo" );
                AssertEquals( "A foo", writer.ToString() );

                
                char[] testBuffer = "Test String".ToCharArray();

                writer.Write( testBuffer, 0, 4 );
                AssertEquals( "A fooTest", writer.ToString() );

                writer.Write( testBuffer, 5, 6 );
                AssertEquals( "A fooTestString", writer.ToString() );
        }

        public void TestNewLine() {
        	
        	StringWriter writer = new StringWriter();
        	
        	writer.NewLine = "\n\r";
        	AssertEquals ("NewLine 1", "\n\r", writer.NewLine);
        	
        	writer.WriteLine ("first");
        	AssertEquals ("NewLine 2", "first\n\r", writer.ToString());
        	
        	writer.NewLine = "\n";
        	AssertEquals ("NewLine 3", "first\n\r", writer.ToString());
        	
        	writer.WriteLine ("second");
        	AssertEquals ("NewLine 4", "first\n\rsecond\n", writer.ToString());
        	
        }
        
        public void TestWriteLine() {
        	
        	StringWriter writer = new StringWriter();
        	writer.NewLine = "\n";
        	
        	writer.WriteLine ("first line");
        	writer.WriteLine ("second line");
        	        	
        	AssertEquals ("WriteLine 1", "first line\nsecond line\n", writer.ToString ());
        	writer.Close ();
        }
        
        public void TestGetStringBuilder() {
        	
        	StringWriter writer = new StringWriter ();
        	writer.Write ("line");
		StringBuilder builder = writer.GetStringBuilder ();
        	builder.Append (12);
        	AssertEquals ("GetStringBuilder 1", "line12", writer.ToString ());
        	writer.Write ("test");
        	AssertEquals ("GetStringBuilder 2", "line12test", builder.ToString ());        	        	
        }
        
        public void TestClose() {
        	
        	StringWriter writer = new StringWriter ();
        	writer.Write ("mono");
        	writer.Close ();
        	
        	try {
        		writer.Write ("kicks ass");
        		Fail ("Close 1");
        	} catch (Exception e) {
        		AssertEquals ("Close 2", typeof (ObjectDisposedException), e.GetType ());
        	}
        }

}

}
