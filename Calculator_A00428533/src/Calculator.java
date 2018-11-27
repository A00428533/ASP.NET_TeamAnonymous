import java.util.Scanner;

public class Calculator {
	
	public static void add(int a,int b)
	{   
		int res=0;
		res = a + b;
		System.out.println(res);
	}
	
	public static void subtract(int a,int b)
	{   
		int res=0;
		res = a - b;
		System.out.println(res);

	}
	
	public static void main(String args[]) {
	
	 int a, b, res;
     Scanner scan = new Scanner(System.in);
     System.out.print("Enter Two Numbers : ");
     a = scan.nextInt();
     b = scan.nextInt();
     Calculator.add(a,b);
     Calculator.add(a,b);
     
     

     
	}
}
