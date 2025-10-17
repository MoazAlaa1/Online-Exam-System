Exam =
{
    currentQuestionIndex: 0,
    userAnswers: [],
    correctAnswers: [],
    examData: null,
    ExamId: 0,


    initExam: async (examId) => {
        try {
            const nextButton = document.getElementById('nextBtn');
            const submitButton = document.getElementById('submitBtn');
            const restartButton = document.getElementById('restartBtn');
            Exam.ExamId = examId;
            console.log(Exam.ExamId)
            const response = await fetch('/api/Exam/' + Exam.ExamId);
            console.log("Fist response")
            console.log(response)
            if (!response.ok) {
                throw new Error('Failed to load exam data');
            }
            Exam.examData = await response.json();
            console.log("Fist examData")
            console.log(Exam.examData)
            console.log("Question")
            console.log(Exam.examData.questionsAnswers[0].text)
            console.log(nextButton);

            Exam.displayQuestion(Exam.currentQuestionIndex);
            console.log("here 012");
            Exam.updateProgressBar();

            nextButton.addEventListener('click', Exam.handleNextQuestion);
            submitButton.addEventListener('click', Exam.handleSubmitExam);
            restartButton.addEventListener('click', Exam.restartExam);

        }
        catch (error) {
            console.error('Error initializing exam:', error);
            alert('Failed to load exam. Please refresh the page.');
        }
    },


    displayQuestion: (index) => {
        console.log("here 0");
        const questionNumberElement = document.getElementById('questionNumber');
        const questionTextElement = document.getElementById('questionText');
        const optionsContainerElement = document.getElementById('optionsContainer');
        const nextButton = document.getElementById('nextBtn');
        const submitButton = document.getElementById('submitBtn');
        const warningElement = document.getElementById('warning');
        const question = Exam.examData.questionsAnswers[index];

        questionNumberElement.textContent = `Question ${index + 1} of ${Exam.examData.questionsAnswers.length}`;
        questionTextElement.textContent = question.text;


        optionsContainerElement.innerHTML = '';

        question.options.forEach(option => {
            const optionElement = document.createElement('div');
            optionElement.className = 'option';
            optionElement.innerHTML = `<input type="radio" id="${option.id}" name="answer" value="${option.id}"><label for="${option.id}">${option.text}</label>`;
            optionsContainerElement.appendChild(optionElement);

            optionElement.addEventListener('click', () => {

                document.querySelectorAll('.option').forEach(opt => {
                    opt.classList.remove('selected');
                });

                optionElement.classList.add('selected');
                document.getElementById(option.id).checked = true;

                warningElement.style.display = 'none';
            });
        });

        if (index === Exam.examData.questionsAnswers.length - 1) {
            nextButton.style.display = 'none';
            submitButton.style.display = 'block';
        }
        else {
            nextButton.style.display = 'block';
            submitButton.style.display = 'none';
        }
        console.log("here 1");
    },

    updateProgressBar: () => {
        const progressBar = document.getElementById('progressBar');
        const progress = ((Exam.currentQuestionIndex + 1) / Exam.examData.questionsAnswers.length) * 100;
        progressBar.style.width = `${progress}%`;
        console.log("here 2");
    },

    handleNextQuestion: async () => {
        const nextButton = document.getElementById('nextBtn');
        const warningElement = document.getElementById('warning');
        const loadingIndicator = document.getElementById('loadingIndicator');
        const selectedOption = document.querySelector('input[name="answer"]:checked');

        if (!selectedOption) {
            warningElement.style.display = 'block';
            return;
        }

        loadingIndicator.style.display = 'block';
        nextButton.disabled = true;

        try {

            const response = await fetch('/api/Exam/SaveAnswer', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    questionId: Exam.examData.questionsAnswers[Exam.currentQuestionIndex].id,
                    choiceId: selectedOption.value
                })
            });

            if (!response.ok) {
                throw new Error('Failed to save answer');
            }
            console.log("Respons")
            console.log(response)

            Exam.userAnswers.push(selectedOption.value);
            console.log("userAnswers")
            console.log(Exam.userAnswers)

            Exam.currentQuestionIndex++;
            Exam.displayQuestion(Exam.currentQuestionIndex);
            console.log("displayQuestion")
            Exam.updateProgressBar();
            console.log("updateProgressBar")
        } catch (error) {
            console.error('Error saving answer:', error);
            alert('Failed to save your answer. Please try again.');
        } finally {
            loadingIndicator.style.display = 'none';
            nextButton.disabled = false;
        }
    },

    handleSubmitExam: async () => {
        const submitButton = document.getElementById('submitBtn');
        const warningElement = document.getElementById('warning');
        const loadingIndicator = document.getElementById('loadingIndicator');
        const selectedOption = document.querySelector('input[name="answer"]:checked');

        if (!selectedOption) {
            warningElement.style.display = 'block';
            return;
        }

        loadingIndicator.style.display = 'block';
        submitButton.disabled = true;

        try {

            const answerResponse = await fetch('/api/Exam/SaveAnswer', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    questionId: Exam.examData.questionsAnswers[Exam.currentQuestionIndex].id,
                    choiceId: selectedOption.value
                })
            });

            if (!answerResponse.ok) {
                throw new Error('Failed to save answer');
            }

            Exam.userAnswers.push(selectedOption.value);

            const resultResponse = await fetch('/api/Exam/SubmitExam/' + Exam.ExamId);
            console.log("resultResponse");
            console.log(resultResponse);
            if (!resultResponse.ok) {
                throw new Error('Failed to submit exam');
            }

            const result = await resultResponse.json();
            console.log("result");
            console.log(result);
            console.log("score");
            console.log(result.score);
            console.log(result.passed);
            Exam.displayResults(result.score, result.passed);
        }
        catch (error) {
            console.error('Error submitting exam:', error);
            alert('Failed to submit exam. Please try again.');
        } finally {
            loadingIndicator.style.display = 'none';
            submitButton.disabled = false;
        }
    },


    displayResults: (percentage, passed) => {
        const nextButton = document.getElementById('nextBtn');
        const submitButton = document.getElementById('submitBtn');
        const scoreDisplay = document.getElementById('scoreDisplay');
        const resultMessage = document.getElementById('resultMessage');
        const resultIcon = document.getElementById('resultIcon');

        document.getElementById('questionContainer').style.display = 'none';
        nextButton.style.display = 'none';
        submitButton.style.display = 'none';

        resultContainer.style.display = 'block';

        scoreDisplay.textContent = `${percentage}%`;

        if (passed == 1) {
            resultIcon.textContent = '✓';
            resultIcon.className = 'result-icon pass';
            resultMessage.textContent = 'Congratulations! You passed the exam.';
        } else {
            resultIcon.textContent = '✗';
            resultIcon.className = 'result-icon fail';
            resultMessage.textContent = 'Sorry, you did not pass the exam. Please try again.';
        }
    },


    restartExam: async () => {
        try {
            await fetch('/api/Exam/ResetExam', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                }
            });


            Exam.currentQuestionIndex = 0;
            Exam.userAnswers = [];
            const progressBar = document.getElementById('progressBar');
            const resultContainer = document.getElementById('resultContainer');

            resultContainer.style.display = 'none';

            document.getElementById('questionContainer').style.display = 'block';

            progressBar.style.width = '0%';

            await Exam.initExam(Exam.ExamId);
        } catch (error) {
            console.error('Error restarting exam:', error);
            alert('Failed to restart exam. Please refresh the page.');
        }
    }
}