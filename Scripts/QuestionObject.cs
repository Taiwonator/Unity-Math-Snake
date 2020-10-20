using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class QuestionObject : MonoBehaviour {

	public string question;
	public string correctAns;
	public List<string> incorrectAns;

	public QuestionObject(string ques, string corr, List<string> inco){
		this.question = ques;
		this.correctAns = corr;
		this.incorrectAns = inco;
	} 

//	public QuestionObject(QuestionObject info) {
//		this.question = info.question;
//		this.correctAns = info.correctAns;
//		this.incorrectAns = info.incorrectAns;
//	}

	public Question_Data CreateData(QuestionObject question){
		return new Question_Data (question.question, question.correctAns, question.incorrectAns);
	}

	public void Restore(Question_Data data){
		this.question = data.question;
		this.correctAns = data.correctAns;
		this.incorrectAns = data.incorrectAns;
	}

	public QuestionObject(){
	
	}
}


