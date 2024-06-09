const { exec } = require('child_process');
const path = require('path');

function convertToHLS(inputFile, outputDir) {
  return new Promise((resolve, reject) => {
    const command = `ffmpeg -i ${inputFile} -codec: copy -start_number 0 -hls_time 10 -hls_list_size 0 -f hls ${path.join(outputDir, 'output.m3u8')}`;

    exec(command, (error, stdout, stderr) => {
      if (error) {
        reject(`FFmpeg Error: ${error.message}`);
      } else if (stderr) {
        reject(`FFmpeg Stderr: ${stderr}`);
      } else {
        resolve(stdout);
      }
    });
  });
}

module.exports = {
  convertToHLS,
};
