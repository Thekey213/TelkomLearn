import { quizzies } from "../data/play-quiz-data.js";


const questionspace = document.querySelector('.question-number');
const typeQuestion = document.querySelector('.question-type');
const answerButton = document.querySelector('.answer-container');
const next = document.querySelector('.next');
const winAnime = document.querySelector('.win-anime');
const lossAnime = document.querySelector('.loss-anime');

let theQuestionNum = 0;
let score = 0;
let average = 0;

function startQuiz(){
  theQuestionNum = 0;
  score = 0;
  next.innerHTML = "Next";
  appearQuestion();
}

function appearQuestion(){
  resetState();
  let currentQuestion = quizzies[theQuestionNum];
  let questionNo = theQuestionNum + 1;
  questionspace.innerHTML = "Question " + questionNo;
  typeQuestion.innerHTML = currentQuestion.questionType;

  currentQuestion.answer.forEach((answer) => {
    const button = document.createElement('button');
    button.classList.add('answers');
    button.innerText = answer.text;
    button.dataset.correct = answer.correct.toString();
    button.addEventListener('click', selectAnswer);
    answerButton.appendChild(button);
  });
}

function resetState(){
  winAnime.style.display = 'none';
  lossAnime.style.display = 'none';
  next.style.display = 'none';
  while(answerButton.firstChild){
    answerButton.removeChild(answerButton.firstChild);
  }
}

function selectAnswer(event){
  const selectedButton = event.target;
  const isCorrect = selectedButton.dataset.correct === 'true';
  if(isCorrect){
    selectedButton.classList.add('correct');
    score++;
    average = (((score / quizzies.length ) * 100).toFixed(1)) 
    winAnime.style.display = 'block';
    lossAnime.style.display = 'none';
  } else {
    selectedButton.classList.add('incorrect');
    lossAnime.style.display = 'block';
    winAnime.style.display = 'none';
  }
  Array.from(answerButton.children).forEach((button) => {
    if(button.dataset.correct === 'true'){
      button.classList.add('correct');
    }
    button.disabled = true;
  });
  next.style.display = 'block';
}

function showScore(){
  resetState();
  questionspace.innerHTML = "Here are your Results";
  typeQuestion.innerHTML = `You got ${score} questions correct out of ${quizzies.length} questions. 
  <div class="total-average"></div>
  <p class="average">${average}%</p>
  `;
  if(average < 50){
    lossAnime.style.display = 'block';
    winAnime.style.display = 'none';
  }else{
    winAnime.style.display = 'block';
    lossAnime.style.display = 'none';
  }

  next.innerHTML = 'Restart';
  next.style.display = 'block';
}

function handleNextButton(){
  theQuestionNum++;
  if (theQuestionNum < quizzies.length){
    appearQuestion();
  } else {
    showScore();
  }
}

next.addEventListener('click', () => {
  if(theQuestionNum < quizzies.length){
    handleNextButton();
  } else {
    startQuiz();
  }
});

startQuiz();
