using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCustomQuestions : MonoBehaviour {

	[SerializeField]
	public List<QuestionObject> CustomQuestions;

	public static CreateCustomQuestions static_questions;

	// Use this for initialization
	void Awake () {

		CreateQuestions ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateQuestions() {
		static_questions = this;

		QuestionObject q1 = new QuestionObject ();
		q1.question = "dirivative of sin(ax)";
		q1.correctAns = "acosx(ax)";
		q1.incorrectAns = new List<string> ();
		q1.incorrectAns.Add("asin(ax)");
		q1.incorrectAns.Add("(1/a)sin(ax)");
		q1.incorrectAns.Add("(1/a)cos(ax)");

		QuestionObject q2 = new QuestionObject ();
		q2.question = "How old am I?";
		q2.correctAns = "17";
		q2.incorrectAns = new List<string> ();
		q2.incorrectAns.Add("16");
		q2.incorrectAns.Add("20");
		q2.incorrectAns.Add("18");

		QuestionObject q3 = new QuestionObject ();
		q3.question = "Best hot beverage?";
		q3.correctAns = "Holy Spirit";
		q3.incorrectAns = new List<string> ();
		q3.incorrectAns.Add("Coffee");
		q3.incorrectAns.Add("Tea");
		q3.incorrectAns.Add("Milk");

		QuestionObject q4 = new QuestionObject ();
		q4.question = "Best actor?";
		q4.correctAns = "Jamie Fox";
		q4.incorrectAns = new List<string> ();
		q4.incorrectAns.Add("Eddie Murphy");
		q4.incorrectAns.Add("Will Smith");
		q4.incorrectAns.Add("Tyrone Gibson");

		QuestionObject q5 = new QuestionObject ();
		q5.question = "15 mod 2";
		q5.correctAns = "1";
		q5.incorrectAns = new List<string> ();
		q5.incorrectAns.Add("15");
		q5.incorrectAns.Add("2");
		q5.incorrectAns.Add("0");

		QuestionObject q6 = new QuestionObject ();
		q6.question = "Pettles on 4 leaf clover";
		q6.correctAns = "4";
		q6.incorrectAns = new List<string> ();
		q6.incorrectAns.Add("3");
		q6.incorrectAns.Add("2");
		q6.incorrectAns.Add("5");

		QuestionObject q7 = new QuestionObject ();
		q7.question = "5 div 2";
		q7.correctAns = "2";
		q7.incorrectAns = new List<string> ();
		q7.incorrectAns.Add("5");
		q7.incorrectAns.Add("1");
		q7.incorrectAns.Add("10");

		QuestionObject q8 = new QuestionObject ();
		q8.question = "Is Birdbox a good film";
		q8.correctAns = "Yes";
		q8.incorrectAns = new List<string> ();
		q8.incorrectAns.Add("No");
		q8.incorrectAns.Add(" ");
		q8.incorrectAns.Add(" ");

		QuestionObject q9 = new QuestionObject ();
		q9.question = "How do I bath myself";
		q9.correctAns = "Shower";
		q9.incorrectAns = new List<string> ();
		q9.incorrectAns.Add("Bucket");
		q9.incorrectAns.Add("Bath tub");
		q9.incorrectAns.Add("In the river");

		QuestionObject q10 = new QuestionObject ();
		q10.question = "My dinomination";
		q10.correctAns = "Pentacostal";
		q10.incorrectAns = new List<string> ();
		q10.incorrectAns.Add("Catholic");
		q10.incorrectAns.Add("Protestant");
		q10.incorrectAns.Add("Orthodox");

		CustomQuestions.Add (q1);
		CustomQuestions.Add (q2);
		CustomQuestions.Add (q3);
		CustomQuestions.Add (q4);
		CustomQuestions.Add (q5);
		CustomQuestions.Add (q6);
		CustomQuestions.Add (q7);
		CustomQuestions.Add (q8);
		CustomQuestions.Add (q9);
		CustomQuestions.Add (q10);
	}
}
