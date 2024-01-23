import { useParams } from "react-router-dom";


export function QuestionBankEdit() {
    const { bankID } = useParams();

    return <div>{bankID}</div>;
}