import { useState, useEffect } from "react";
import "./candidates.scss";
import {
  ICompany,
  ICreateCandidateDTo,
  ICreateCompanyDto,
  ICreateJobDto,
  IJob,
} from "../../types/global.typing";
import {
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import httpModule from "../../helpers/http.module";
import { error } from "console";
import { appendFile } from "fs";

const AddCandidate = () => {
  const [candidate, setCandidate] = useState<ICreateCandidateDTo>({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    coverLetter: "",
    jobId: "",
  });

  const [jobs, setJobs] = useState<IJob[]>([]);
  const [pdfFile, setPdfFile] = useState<File | null>();
  const redirect = useNavigate();

  //Populating job list from Company Page

  useEffect(() => {
    httpModule
      .get<IJob[]>("/Job/Get")
      .then((response) => {
        setJobs(response.data);
      })
      .catch((error) => {
        alert("Error");
        console.log(error);
      });
  }, []);

  const handleClickSaveBtn = () => {
    if (
      candidate.firstName === "" ||
      candidate.lastName === "" ||
      candidate.email === "" ||
      candidate.phoneNumber === "" ||
      candidate.coverLetter === "" ||
      candidate.jobId === "" ||
      !pdfFile
    ) {
      alert("Field is required");
      return;
    }

    const newCandidaateFormData = new FormData();
    newCandidaateFormData.append("firstName", candidate.firstName);
    newCandidaateFormData.append("lastName", candidate.lastName);
    newCandidaateFormData.append("email", candidate.email);
    newCandidaateFormData.append("phoneNumber", candidate.phoneNumber);
    newCandidaateFormData.append("coverLetter", candidate.coverLetter);
    newCandidaateFormData.append("jobId", candidate.jobId);
    newCandidaateFormData.append("pdfFile", pdfFile);

    httpModule
      .post("/Candidate/Create", newCandidaateFormData, {})
      .then((response) => redirect("/candidates"))
      .catch((error) => console.log(error));
  };

  const handleClickBackBtn = () => {
    redirect("/candidates");
  };

  return (
    <div className="content">
      <div className="add-candidate">
        <h2>Add a new candidate</h2>
        <TextField
          fullWidth
          autoComplete="off"
          label="First Name"
          variant="outlined"
          value={candidate.firstName}
          onChange={(n) =>
            setCandidate({ ...candidate, firstName: n.target.value })
          }
        />
        <TextField
          fullWidth
          autoComplete="off"
          label="Last Name"
          variant="outlined"
          value={candidate.lastName}
          onChange={(n) =>
            setCandidate({ ...candidate, lastName: n.target.value })
          }
        />
        <TextField
          fullWidth
          autoComplete="off"
          label="Email"
          variant="outlined"
          value={candidate.email}
          onChange={(n) =>
            setCandidate({ ...candidate, email: n.target.value })
          }
        />
        <TextField
          fullWidth
          autoComplete="off"
          label="Phone Number"
          variant="outlined"
          value={candidate.phoneNumber}
          onChange={(n) =>
            setCandidate({ ...candidate, phoneNumber: n.target.value })
          }
        />
        <TextField
          fullWidth
          autoComplete="off"
          label="Cover Letter"
          variant="outlined"
          value={candidate.coverLetter}
          onChange={(n) =>
            setCandidate({ ...candidate, coverLetter: n.target.value })
          }
          multiline
        />
        <FormControl fullWidth>
          <InputLabel>Job Type</InputLabel>
          <Select
            label="Job Type"
            value={candidate.jobId}
            onChange={(s) =>
              setCandidate({ ...candidate, jobId: s.target.value })
            }
          >
            {jobs.map((item) => (
              <MenuItem key={item.id} value={item.id}>
                {item.title}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <input
          type="file"
          onChange={(event) =>
            setPdfFile(event.target.files ? event.target.files[0] : null)
          }
        />

        <div className="btns">
          <Button
            variant="outlined"
            color="primary"
            onClick={handleClickSaveBtn}
          >
            Save
          </Button>
          <Button
            variant="outlined"
            color="secondary"
            onClick={handleClickBackBtn}
          >
            Back
          </Button>
        </div>
      </div>
    </div>
  );
};

export default AddCandidate;
