pipeline {
    agent any

    environment {
        TEST_PROJECT = 'C:/Users/Mutahir/source/repos/POM_Mobile_App_Automate_Stage/SanityMain.csproj'
        ALLURE_RESULTS = 'allure-results'
    }

    stages {
        stage('Restore NuGet Packages') {
            steps {
                echo 'Restoring NuGet packages...'
                bat "dotnet restore ${env.TEST_PROJECT}"
            }
        }

        stage('Build Project') {
            steps {
                echo 'Building the project...'
                bat "dotnet build ${env.TEST_PROJECT} --configuration Release"
            }
        }

        stage('Run Tests') {
            steps {
                echo 'Running Appium tests with NUnit...'
                bat "dotnet test ${env.TEST_PROJECT} --configuration Release --logger:\"nunit;LogFilePath=${env.ALLURE_RESULTS}\\TestResult.xml\" --results-directory ${env.ALLURE_RESULTS}"
            }
        }

        stage('Generate Allure Report') {
            steps {
                echo 'Generating Allure report...'
                bat "allure generate ${env.ALLURE_RESULTS} --clean -o ${env.ALLURE_RESULTS}/report"
            }
        }

        stage('Publish Allure Report') {
            steps {
                echo 'Publishing Allure report in Jenkins...'
                allure([
                    includeProperties: false,
                    jdk: '',
                    results: [[path: "${env.ALLURE_RESULTS}"]]
                ])
            }
        }
    }

    post {
        always {
            echo 'Cleaning workspace...'
            cleanWs()
        }
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed. Check logs for errors.'
        }
    }
}